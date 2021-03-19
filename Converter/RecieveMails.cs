using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Converter
{
    class RecieveMails
    {
        public static Tuple<string, string> Invoke(string pdf_path, string hostname, int port, string username, string password)
        {

            using (ImapClient client = new ImapClient())
            {
                
                client.Connect(hostname, port, true);
                client.Authenticate(username, password);

                var folder = client.Inbox;
                folder.Open(MailKit.FolderAccess.ReadWrite);

                int unread = folder.FirstUnread;
                if (unread < 0)
                {
                    throw new Exception("Could not find any new mails. Waiting for new mails...");
                }

                int[] arr = new int[1];
                arr[0] = unread;
                var items = folder.Fetch(arr, MessageSummaryItems.Flags);

                foreach (var item in items)
                {
                    if (item.Flags.Value == MessageFlags.Seen)
                        throw new Exception("Could not find any new unread mails. Waiting for new mails...");
                }


                var message = folder.GetMessage(unread);
                if(message == null)
                    throw new Exception("Could not find any new unread mails. Waiting for new mails...");

                string name = message.Subject;
                string content = message.HtmlBody;
                content = HtmlToPlainText(content);

                if (message.Attachments == null)
                {
                    throw new Exception("Could not find any mail attachements.");
                }
                else
                {
                    foreach (MimeEntity attachment in message.Attachments)
                    {
                        string PdfFile = pdf_path + "\\" + name + ".pdf";
                        var fileName = PdfFile;

                        using (var stream = File.Create(fileName))
                        {
                            if (attachment is MessagePart)
                            {
                                var rfc822 = (MessagePart)attachment;

                                rfc822.Message.WriteTo(stream);
                            }
                            else
                            {
                                var part = (MimePart)attachment;

                                part.Content.DecodeTo(stream);
                            }
                        }
                    }
                    
                }

                folder.AddFlags(unread, MailKit.MessageFlags.Seen, true);
                return new Tuple<string, string>(name, content);
            }
        }

        private static string HtmlToPlainText(string html)
        {
            const string normalLineBreak = @"(\n|\r)";
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            //const string tagWhiteSpace = @"(>|$)(\W)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR|/p)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            //const string lineBreak = @"";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var normalLineBreakRegex = new Regex(normalLineBreak, RegexOptions.Multiline);
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);

            // remove linebreaks
            text = normalLineBreakRegex.Replace(text, " ");
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
    }
}
