﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anonimize.Services;

namespace Tests.Assembly.Services
{
    class BaseCryptoService : ICryptoService
    {
        public byte[] Decrypt(byte[] inputBuffer)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string input)
        {
            throw new NotImplementedException();
        }

        public byte[] Encrypt(byte[] inputBuffer)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string input)
        {
            throw new NotImplementedException();
        }
    }
}
