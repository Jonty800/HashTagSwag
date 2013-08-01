using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fCraft;
using fCraft.Events;
using System.Text.RegularExpressions;

namespace HashTagSwag {
    public class Init : Plugin {

        public void Initialize () {
            Logger.Log( LogType.ConsoleOutput, "Started HashTagSwag" + Version );
            Chat.Sending += ChatSending;
        }

        public static void ChatSending ( object sender, ChatSendingEventArgs e ) {
            if ( e.Message == null ) return;
            if ( e.Player == null ) return;

            //Parse greentext
            if ( e.Message.Length > 1 && e.MessageType == ChatMessageType.Global ) {
                if ( e.Message.StartsWith( ">" ) && e.Message[1] != ' ' ) {
                    e.FormattedMessage = String.Format( "{0}&F: {1}{2}", e.Player.ClassyName, Color.Lime, e.Message );
                    return;
                }
            }
            //Parse all others
            string Message = e.Message; //use only e.Message so you don't accidentally highlight user names ect
            Message = HighlightSections( Message );
            e.FormattedMessage = String.Format( "{0}&F: {1}", e.Player.ClassyName, Message ); //reformat the message 
        }

        /// <summary>
        /// Highlights HashTags in blue (&9)
        /// Highlights @Users in Red (&C)
        /// Highlights urls in Yellow (&E)
        /// </summary>
        /// <param name="input">String in question</param>
        /// <returns>Replaced string</returns>
        public static String HighlightSections ( String input ) {
            String result = Regex.Replace( input, @"(?:(?<=\s)|^)@(\w*[A-Za-z_]+\w*)", @"&C$0&F" );
            result = Regex.Replace( result, @"(?:(?<=\s)|^)#(\w*[A-Za-z_]+\w*)", @"&9$0&F" );
            result = Regex.Replace( result, @"\bhttps?://[-\w]+(\.\w[-\w]*)+(:\d+)?(/[^.!,?;""\'<>()\[\]\{\}\s\x7F-\xFF]*([.!,?]+[^.!,?;""\'<>\(\)\[\]\{\}\s\x7F-\xFF]+)*)?\b", @"&E$0&F", RegexOptions.IgnoreCase );
            return result;
        }

        public string Name {
            get {
                return "HashTagSwag";
            }
            set {
                Name = value;
            }
        }

        public string Version {
            get {
                return "1.0";
            }
            set {
                Version = value;
            }
        }
    }
}
