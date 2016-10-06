using MsgPack;
using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Network.MessagePack
{
    public class ReceiveMessage
    {
        public string Type;
#pragma warning disable CS0169
        private MemoryStream _Stream;
#pragma warning restore CS0169
        public Dictionary<string, MessagePackObject> Map { get; private set; }

        public MemoryStream Stream
        {
            get { return _Stream; }
            set
            {
                _Stream = value;
                OpenMessagePack();
            }
        }

      private void OpenMessagePack()
        {
            try
            {
                var rawObject = Unpacking.UnpackObject(_Stream);

                if (!rawObject.IsDictionary)
                    throw new Exception.NoDictionaryFoundException();
                var tmp = rawObject.AsDictionary();
                foreach (var item in tmp)
                {
                    Map[(string)item.Key] = item.Value;
                    Log.Info((string)item.Key, item.Value);
                }
            }
            catch (System.Exception ex)
            {
                Log.Error("OpenMessagePack", ex);
            }
        }

      
        public ReceiveMessage(MemoryStream stream)
        {
            Map = new Dictionary<string, MessagePackObject>();
            Stream = stream;
        }

        public ReceiveMessage(Dictionary<string, MessagePackObject> map)
        {
            this.Map = map;
        }

        public string GetString(string key)
        {
            MessagePackObject value;
            if (!Map.TryGetValue(key, out value))
                throw new Exception.NoAttributeFoundException(key);
            try
            {
                if (!value.UnderlyingType.Equals(typeof(string)))
                    throw new Exception.AttributeBadTypeException(key, value.UnderlyingType);
                return (string)value;
            }
#pragma warning disable CS0168
            catch (System.NullReferenceException ex)
#pragma warning restore CS0168
            {
                throw new Exception.AttributeEmptyException(key);
            }
        }


        public byte GetByte(string key)
        {
            MessagePackObject value;
            try
            {
                if (!Map.TryGetValue(key, out value))
                    throw new Exception.NoAttributeFoundException(key);

                if (!value.UnderlyingType.Equals(typeof(byte)))
                    throw new Exception.AttributeBadTypeException(key, value.UnderlyingType);
                return value.AsByte();
            }
#pragma warning disable CS0168
            catch (System.NullReferenceException ex)
#pragma warning restore CS0168
            {
                throw new Exception.AttributeEmptyException(key);
            }
        }


        public int GetInt(string key)
        {
            MessagePackObject value;
            try
            {
                if (!Map.TryGetValue(key, out value))
                    throw new Exception.NoAttributeFoundException(key);

                if (!value.UnderlyingType.Equals(typeof(int)))
                    throw new Exception.AttributeBadTypeException(key, value.UnderlyingType);
                return value.AsInt32();
            }
#pragma warning disable CS0168
            catch (System.NullReferenceException ex)
#pragma warning restore CS0168
            {
                throw new Exception.AttributeEmptyException(key);
            }
        }

        public float GetFloat(string key)
        {
            MessagePackObject value;
            try
            {
                if (!Map.TryGetValue(key, out value))
                    throw new Exception.NoAttributeFoundException(key);

                if (!value.UnderlyingType.Equals(typeof(float)))
                    throw new Exception.AttributeBadTypeException(key, value.UnderlyingType);
                return value.AsSingle();
            }
#pragma warning disable CS0168
            catch (System.NullReferenceException ex)
#pragma warning restore CS0168
            {
                throw new Exception.AttributeEmptyException(key);
            }
        }

        public bool GetBoolean(string key)
        {
            MessagePackObject value;
            try
            {
                if (!Map.TryGetValue(key, out value))
                    throw new Exception.NoAttributeFoundException(key);

                if (!value.UnderlyingType.Equals(typeof(bool)))
                    throw new Exception.AttributeBadTypeException(key, value.UnderlyingType);
                return value.AsBoolean();
            }
#pragma warning disable CS0168
            catch (System.NullReferenceException ex)
#pragma warning restore CS0168
            {
                throw new Exception.AttributeEmptyException(key);
            }
        }

        public ReceiveMessage GetStruct(string key)
        {
            MessagePackObject value;
            if (!Map.TryGetValue(key, out value))
                throw new Exception.NoAttributeFoundException(key);
            try
            {
                if (!value.IsDictionary)
                    throw new Exception.AttributeBadTypeException(key, value.UnderlyingType);
                Dictionary<string, MessagePackObject> ret = new Dictionary<string, MessagePackObject>();
                foreach (var item in value.AsDictionary())
                {
                    ret[(string)item.Key] = item.Value;
                }
                return new ReceiveMessage(ret);
            }
#pragma warning disable CS0168
            catch (System.NullReferenceException ex)
#pragma warning restore CS0168
            {
                throw new Exception.AttributeEmptyException(key);
            }
        }

     
        public string GetMessageType()
        {
            return GetString("type");
        }

        public string GetMessageId()
        {
            return GetString("id");
        }
        
    }
}