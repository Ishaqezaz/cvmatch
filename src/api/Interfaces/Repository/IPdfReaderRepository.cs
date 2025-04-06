using System;


namespace api.Interfaces.Repository
{
    public interface IPdfReaderRepository
    {
        public string ReadText(Stream stram);
    }
}

