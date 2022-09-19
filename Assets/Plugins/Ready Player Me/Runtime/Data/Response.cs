namespace ReadyPlayerMe
{
    public struct Response
    {
        public string Text;
        public byte[] Data;
        public string LastModified;
        public long ByteLength;

        public Response(string text, byte[] data, string lastModified, long byteLength)
        {
            Text = text;
            Data = data;
            LastModified = lastModified;
            ByteLength = byteLength;
        }
    }
}
