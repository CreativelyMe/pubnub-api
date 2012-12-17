using System;
using PubNub_Messaging;
using NUnit.Framework;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;


namespace PubNubTest
{
    [TestFixture]
    public class WhenSubscribedToAChannel
    {
       [Test]
       public void ThenItShouldReturnReceivedMessage () 
       {

          Pubnub pubnub = new Pubnub (
                "demo",
                "demo",
                "",
                "",
                false);
          string channel = "hello_world";

          Common cm = new Common();
          cm.deliveryStatus = false;
          cm.objResponse = null;

          pubnub.subscribe (channel, cm.DisplayReturnMessage); 
          Thread.Sleep(3000);
          string msg = "Test Message";
          cm.objResponse = null;
          pubnub.publish (channel, msg, DisplayReturnMessageDummmy);
          cm.deliveryStatus = false;
          while (!cm.deliveryStatus);
             if (cm.objResponse != null) {
                IList<object> fields = cm.objResponse as IList<object>;

                if (fields [0] != null)
                {
                    var myObjectArray = (from item in fields select item as object).ToArray ();
                    Assert.AreEqual(msg, myObjectArray[0].ToString());
                }
             }
       }
        
       [Test]
       public void ThenItShouldReturnReceivedMessageCipher ()
       {

          Pubnub pubnub = new Pubnub (
                "demo",
                "demo",
                "",
                "enigma",
                false);
          string channel = "hello_world";

          Common cm = new Common();
          cm.deliveryStatus = false;
          cm.objResponse = null;
          pubnub.subscribe (channel, cm.DisplayReturnMessage); 
          Thread.Sleep(3000);
          string msg = "Test Message";
          pubnub.publish (channel, msg, DisplayReturnMessageDummmy);
          cm.deliveryStatus = false;
          while (!cm.deliveryStatus);
            
            if (cm.objResponse != null) {
              IList<object> fields = cm.objResponse as IList<object>;
              
              if (fields [0] != null)
              {
                var myObjectArray = (from item in fields select item as object).ToArray ();
                Console.WriteLine ("Resp:" + myObjectArray[0].ToString ());
                Assert.AreEqual(msg, myObjectArray[0].ToString());
              }
          }
       }

       public static void DisplayReturnMessageDummmy(object result)
        {
            IList<object> message = result as IList<object>;

            if (message != null && message.Count >= 2)
            {
                for (int index = 0; index < message.Count; index++)
                {
                    //ParseObject(message[index], 1);
                }
            }
            else
            {
                Console.WriteLine("unable to parse data");
            }
            //deliveryStatus = true;
            //objResponse = result;
        }
    }
}

