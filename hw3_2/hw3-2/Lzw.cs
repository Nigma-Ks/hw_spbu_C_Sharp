using System;
namespace hw3_2
{
    public class Lzw
    {
        private BorStringStorage _storage;

        private void AddAllSymbolsToStorage()
        {
            for (int i = 0; i < 256; i++)
            {
                string symbol = "";
                symbol += (char)i;
                _storage.Add(symbol);
            }
        }

        private void AddAllSymbolsToList(List<string> list)
        {
            for (int i = 0; i < 256; i++)
            {
                string symbol = "";
                symbol += (char)i;
                list.Add(symbol);
            }
        }

        public (List<Byte[]> bytesOfCompressedText, bool isEmpty) LzwCompression(string? textForCompression)
        {
            _storage = new BorStringStorage();
            bool isEmpty = false;
            List<Byte[]> codesOfCompressedText = new();
            if (String.IsNullOrEmpty(textForCompression))
            {
                isEmpty = true;
                return (codesOfCompressedText, isEmpty);
            }

            AddAllSymbolsToStorage();
            int textForCompressionLenght = textForCompression.Length;
            int currentCode = 0; //0 never used because entire alphabet is already in storage
            string currentSuffix = "";
            for (int i = 0; i < textForCompressionLenght; i++)
            {
                if (_storage.Size > 2147483647) //максимальное число int, дальше хранилище будет рабоать некорректно
                {

                }
                currentSuffix += textForCompression[i];
                var (isInStorage, code) = _storage.Contains(currentSuffix);
                if (isInStorage)
                {
                    currentCode = code;
                    if (i == textForCompressionLenght - 1) //if last suffix is already in storage
                    {
                        codesOfCompressedText.Add(BitConverter.GetBytes(Convert.ToUInt32(currentCode)));
                    }
                }
                else
                {
                    _storage.Add(currentSuffix);
                    codesOfCompressedText.Add(BitConverter.GetBytes(Convert.ToUInt32(currentCode)));
                    currentSuffix = "";
                    currentSuffix += textForCompression[i];
                    (isInStorage, currentCode) = _storage.Contains(currentSuffix); //only one symbol, it will be in storage
                    if (i == textForCompressionLenght - 1) //if last suffix is new 
                    {
                        codesOfCompressedText.Add(BitConverter.GetBytes(Convert.ToUInt32(currentCode)));
                    }
                }
            }

            return (codesOfCompressedText, isEmpty);
        }

        public (string decompressedText, bool isEmptyText, bool isCorrectText) LzwDecompression(List<Byte[]> compressedMessage)
        {
            string decompressedText = "";
            int amountOfSuffixesInCompressedText = compressedMessage.Count;
            if (amountOfSuffixesInCompressedText == 0)
            {
                return ("", true, true);
            }

            List<string> suffixes = new();
            string lastSuffix = ""; //unnecessary initialization
            AddAllSymbolsToList(suffixes);
            for (int i = 0; i < amountOfSuffixesInCompressedText; i++)
            {
                UInt32 uIntCode = BitConverter.ToUInt32(compressedMessage[i]);
                int intCode = Convert.ToInt32(uIntCode);
                if (intCode < suffixes.Count)
                {
                    decompressedText += suffixes[intCode];
                    if (i > 0)
                    {
                        suffixes.Add(lastSuffix +
                                     suffixes[intCode][0]); //there is warning here if no initialization
                    }

                    lastSuffix = suffixes[intCode];
                }
                else if (intCode == suffixes.Count)
                {
                    lastSuffix = lastSuffix + lastSuffix[0];
                    suffixes.Add(lastSuffix);
                    decompressedText += lastSuffix;
                }
                else
                {
                    return ("", false, false);
                }
            }

            return (decompressedText, false, true);
        }
    }
}

