using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using VoiceTexterBot.Models;

namespace VoiceTexterBot.Services
{
    public class MemoryStorage : IStorage
    {
        ///Хранилище сессий
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage() 
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId) 
        {
            //Возвращаем сессию по ключу, если она существует
            if (_sessions.ContainsKey(chatId)) 
            {
                return _sessions[chatId];
            }

            //Создаем и возвращаем новую сессию, если ее не существовало
            var newSession = new Session() { LanguageCode = "ru" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
            
    }
}
