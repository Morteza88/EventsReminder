using EventsReminder.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsReminder.Utility
{
    public class SmtpClientDB
    {
        public static SmtpClientModel DefualtSmtpClientModel { get; set; }
        private static string DbFileName;
        public static void init(string dbFileName = @"MySmtpClients.db")
        {
            DbFileName = dbFileName;
            DefualtSmtpClientModel = selectDefualt();
            if (DefualtSmtpClientModel == null)
            {
                DefualtSmtpClientModel = new SmtpClientModel("smpt host", "sender Id (Email)", "password", false);
                insert(DefualtSmtpClientModel);
            }
        }
        public static void insert(SmtpClientModel smtpClient)
        {
            using (var db = new LiteDatabase(DbFileName))
            {
                var smtpClients = db.GetCollection<SmtpClientModel>("smtpClients");
                smtpClients.Insert(smtpClient);
            }
        }
        public static SmtpClientModel selectDefualt()
        {
            using (var db = new LiteDatabase(DbFileName))
            {
                var smtpClients = db.GetCollection<SmtpClientModel>("smtpClients");
                var results = smtpClients.Find(x => x.IsDefualt).ToList();
                if (results.Count == 1)
                {
                    return results[0];
                }
                return null;
            }
        }
        public static IEnumerable<SmtpClientModel> selectAll()
        {
            using (var db = new LiteDatabase(DbFileName))
            {
                var smtpClients = db.GetCollection<SmtpClientModel>("smtpClients");
                return smtpClients.FindAll();
            }
        }
        public static void delete(SmtpClientModel smtpClient)
        {
            using (var db = new LiteDatabase(DbFileName))
            {
                var smtpClients = db.GetCollection<EventModel>("smtpClients");
                smtpClients.Delete(x => x.Id == smtpClient.Id);
            }
        }
        public static void update(SmtpClientModel smtpClient)
        {
            using (var db = new LiteDatabase(DbFileName))
            {
                var smtpClients = db.GetCollection<SmtpClientModel>("smtpClients");
                smtpClients.Update(smtpClient);
            }
        }
    }
}
