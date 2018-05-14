using EventsReminder.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsReminder.Utility
{
    public class EventDB
    {

        private static string DbFileName;
        public static void init(string dbFileName = @"MyEvents.db")
        {
            DbFileName = dbFileName;
        }
        public static void insert(EventModel eventModel)
        {
            using (var db = new LiteDatabase(DbFileName))
            {
                var events = db.GetCollection<EventModel>("events");
                events.Insert(eventModel);
            }
        }
        public static IEnumerable<EventModel> select(DateTime startDate, DateTime endDate)
        {
            using (var db = new LiteDatabase(DbFileName))
            {
                var events = db.GetCollection<EventModel>("events");
                var results = events.Find(x => x.StartDateTime > startDate && x.StartDateTime < endDate);
                return results;
            }
        }
        public static IEnumerable<EventModel> selectAll()
        {
            using (var db = new LiteDatabase(DbFileName))
            {
                var events = db.GetCollection<EventModel>("events");
                return events.FindAll();
            }
        }
        public static void delete(EventModel eventModel)
        {
            using (var db = new LiteDatabase(DbFileName))
            {
                var events = db.GetCollection<EventModel>("events");
                events.Delete(x => x.Id == eventModel.Id);
            }
        }
        public static void update(EventModel eventModel)
        {
            using (var db = new LiteDatabase(DbFileName))
            {
                var events = db.GetCollection<EventModel>("events");
                events.Update(eventModel);
            }
        }
    }
}
