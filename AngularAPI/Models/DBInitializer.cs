using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAPI.Models
{
    public class DBInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Gebruikers.Any())
            {
                return;
            }

            for (int i = 1; i <= 5; i++)
            {
                context.Gebruikers.Add(new Gebruiker { Email = "user" + i + "@mail.com", Gebruikersnaam = "user" + i, Wachtwoord = "user" + i });
                context.SaveChanges();
                context.Lijsten.Add(new Lijst { Naam = "lijst" + i, Beschrijving = "Dit is lijst " + i, StartDatum = new DateTime(2020, 1, 1), EindDatum = new DateTime(2020, 12, 31), GebruikerID = i });
                context.SaveChanges();
                context.Items.Add(new Item { Naam = "item" + i, Beschrijving = "Dit is item " + i, LijstID = i });
                context.Stemmen.Add(new Stem { GebruikerID = i, ItemID = i });
                context.SaveChanges();
            }

            //context.Gebruikers.AddRange(new Gebruiker { Email = "user1@mail.com", Gebruikersnaam = "user1", Wachtwoord = "user1" } );
            //context.SaveChanges();

            //context.Lijsten.AddRange(new Lijst { Beschrijving = "Dit is lijst 1", Naam = "lijst1", StartDatum = new DateTime(), EindDatum = new DateTime() });
            //context.SaveChanges();

            //context.Items.AddRange(new Item { Naam = "item1", Beschrijving = "Dit is item1", Foto = "item1", LijstID = 1 });
            //context.SaveChanges();

            //context.Stemmen.Add(new Stem { GebruikerID = 1, ItemID = 1 });
            //context.SaveChanges();
        }   
    }
}
