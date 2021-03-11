using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;

namespace MailToDocx
{
    class RecieveMails
    {
        public static string f(string pdf_path, string name, string hostname, int port, string username, string password)
        {

            using (ImapClient client = new ImapClient())
            {
                try
                {
                    client.Connect(hostname, port, true);
                    client.Authenticate(username, password);
                }
                catch
                {
                    name = "ERROR: Could not connect to host.";


                    Console.WriteLine(name);
                    return name;
                }

                var folder = client.Inbox;
                folder.Open(MailKit.FolderAccess.ReadWrite);

                int unread = folder.FirstUnread;
                if(unread < 0)
                {
                    return name;
                }

                int[] arr = new int[1];
                arr[0] = unread;
                var items = folder.Fetch(arr, MessageSummaryItems.Flags);
                foreach (var item in items)
                {
                    if (item.Flags.Value == MessageFlags.Seen)
                        return name;
                }


                var message = folder.GetMessage(unread);
                name = message.Subject;
                Console.WriteLine("Found mail.  " + name);

                if(message.Attachments == null)
                {
                    name = "ERROR: Could not find any attached file.";
                }

                else
                {
                    try
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
                    catch
                    {
                        name = "ERROR: Could not read attached file.";
                    }
                }
                
                folder.AddFlags(unread, MailKit.MessageFlags.Seen, true);
                return name;

            }
        }
    }
}
