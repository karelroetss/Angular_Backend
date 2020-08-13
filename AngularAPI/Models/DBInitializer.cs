using Microsoft.AspNetCore.Mvc;
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
            List<Item> items = new List<Item>();

            if (context.Gebruikers.Any())
            {
                return;
            }

            context.Gebruikers.Add(new Gebruiker { Email = "admin@mail.be", Gebruikersnaam = "Admin", Wachtwoord = "admin" });
            context.Gebruikers.Add(new Gebruiker { Email = "voter@mail.be", Gebruikersnaam = "Voter", Wachtwoord = "voter" });
            context.SaveChanges();

            context.Lijsten.Add(new Lijst { Naam = "Favorite sport", Beschrijving = "Vote here for you favorite sport!", StartDatum = new DateTime(2020, 8, 5), EindDatum = new DateTime(2020, 8, 14), GebruikerID = 1 });
            context.Lijsten.Add(new Lijst { Naam = "Best soccer player", Beschrijving = "Vote for who you think is the best soccer player.", StartDatum = new DateTime(2020, 8, 1), EindDatum = new DateTime(2020, 8, 20), GebruikerID = 1 });
            context.Lijsten.Add(new Lijst { Naam = "Best video game", Beschrijving = "Vote for your favorite video game!", StartDatum = new DateTime(2020, 8, 15), EindDatum = new DateTime(2020, 8, 21), GebruikerID = 1 });
            context.Lijsten.Add(new Lijst { Naam = "Trump president", Beschrijving = "Do you think trump is a good president?", StartDatum = new DateTime(2020, 8, 20), EindDatum = new DateTime(2020, 8, 23), GebruikerID = 1 });
            context.Lijsten.Add(new Lijst { Naam = "Cannabis", Beschrijving = "Should cannabis be legal?", StartDatum = new DateTime(2020, 8, 5), EindDatum = new DateTime(2020, 8, 8), GebruikerID = 1 });
            context.SaveChanges();

            context.Items.Add(new Item { LijstID = 1, Naam = "Soccer", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 1, Naam = "Basketball", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 1, Naam = "American football", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 1, Naam = "Lacrosse", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 1, Naam = "Cricket", Beschrijving = "" });

            context.Items.Add(new Item { LijstID = 2, Naam = "Lionel Messi", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 2, Naam = "Kevin De Bruyne", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 2, Naam = "Eden Hazard", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 2, Naam = "Cristiano Ronaldo", Beschrijving = "" });

            context.Items.Add(new Item { LijstID = 3, Naam = "Elder Scrolls", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 3, Naam = "CS:GO", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 3, Naam = "Fortnite", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 3, Naam = "Minecraft", Beschrijving = "" });

            context.Items.Add(new Item { LijstID = 4, Naam = "Yes", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 4, Naam = "No", Beschrijving = "" });

            context.Items.Add(new Item { LijstID = 5, Naam = "Yes", Beschrijving = "" });
            context.Items.Add(new Item { LijstID = 5, Naam = "No", Beschrijving = "" });
            context.SaveChanges();

            context.Stemmen.Add(new Stem { GebruikerID = 2, ItemID = 1 });

            items = context.Items.ToList();

            foreach(Item item in items)
            {
                var rnd = new Random();
                for (int i = 1; i < rnd.Next(1,10); i++)
                {
                    context.Stemmen.Add(new Stem { GebruikerID = 2, ItemID = item.ItemID });
                }
            }
            context.SaveChanges();
        }
    }
}
