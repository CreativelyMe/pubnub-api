using System;
using PubNub_Messaging;
using System.Collections.Generic;
using System.ComponentModel;

namespace PubNubTest
{
  public class Common
  {
        public object objResponse = null;
        public bool deliveryStatus = false;
        
        public void DisplayReturnMessageDummy(object result)
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

        public void DisplayReturnMessage(object result)
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
            deliveryStatus = true;
            objResponse = result;
        }

        void ParseObject(object result, int loop)
        {
            if (result is object[])
            {
                object[] arrResult = (object[])result;
                foreach (object item in arrResult)
                {
                    if (!item.GetType().IsGenericType)
                    {
                        if (!item.GetType().IsArray)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        else
                        {
                            ParseObject(item, loop + 1);
                        }
                    }
                    else
                    {
                        ParseObject(item, loop + 1);
                    }
                }
            }
            else if (result.GetType().IsGenericType && (result.GetType().Name == typeof(Dictionary<,>).Name))
            {
                Dictionary<string, object> itemList = (Dictionary<string, object>)result;
                foreach (KeyValuePair<string, object> pair in itemList)
                {
                    Console.WriteLine(string.Format("key = {0}", pair.Key));
                    if (pair.Value is object[])
                    {
                        Console.WriteLine("value = ");
                        ParseObject(pair.Value, loop);
                    }
                    else
                    {
                        Console.WriteLine(string.Format("value = {0}", pair.Value));
                    }
                }
            }
            else
            {
                Console.WriteLine(result.ToString());
            }
        }

        public long Timestamp(Pubnub pubnub)
        {
            deliveryStatus = false;

            pubnub.time(DisplayReturnMessage);
            while (!deliveryStatus) ;

            IList<object> fields = objResponse as IList<object>;
            return Convert.ToInt64(fields[0].ToString());
        }
  }
}

