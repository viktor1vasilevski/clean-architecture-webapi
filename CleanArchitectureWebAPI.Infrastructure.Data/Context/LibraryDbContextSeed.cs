using CleanArchitectureWebAPI.Domian.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWebAPI.Infrastructure.Data.Context
{
    public class LibraryDbContextSeed
    {
        public static async Task SeedAsync(LibraryDbContext libraryDbContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // NOTE : Only run this if using a real database
                libraryDbContext.Database.Migrate();
                libraryDbContext.Database.EnsureCreated();

                // seed Soaps
                await SeedSoapsAsync(libraryDbContext);

                // seed Oils
                await SeedOilsAsync(libraryDbContext);

                //// seed Balms
                await SeedBalmsAsync(libraryDbContext);

            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<LibraryDbContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(libraryDbContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static async Task SeedSoapsAsync(LibraryDbContext libraryDbContext)
        {
            if (libraryDbContext.Soaps.Any())
                return;

            var soaps = new List<Soap>()
            {
                new Soap()
                {
                    Edition = "Orange Brick",
                    Brand = "Craftsman",
                    Description = "With the bright, earthy smell of the forest and a hint of lavender," +
                                  " our beard soap delivers a refreshing start to your beard care regiment. " +
                                  "Made from a choice blend of oils, this bar is formulated to build a particularly thick, " +
                                  "creamy lather for a thorough beard cleaning that won't leave your beard or face dry",
                    UnitPrice = 13,
                    UnitQuantity = 100,
                    URL = "https://app.imgforce.com/images/user/bdZ_1611618012_soap-6.jpg",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    CreatedBy = "Viktor"
                },

                new Soap ()
                {
                    Edition = "Brown",
                    Brand = "Honest Amish",
                    Description = "Exclusively Handmade Natural Skin Care Soap Bar for dry skin. " +
                                  "Beautiful handcrafted natural skin care soap bar from Organic Goat milk, " +
                                  "extra virgin organic Coconut, organic Palm and extra virgin Olive oils.",
                    UnitPrice = 11,
                    UnitQuantity = 100,
                    URL = "https://app.imgforce.com/images/user/jyC_1611618012_soap-2.jpg",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    CreatedBy = "Viktor"
                },

                new Soap()
                {
                    Edition = "Light Yellow",
                    Brand = "Crocodile",
                    Description = "Artisanal soap handmade in Ojai, California with locally farmed fresh raw " +
                                  "Goat Milk and Organic Essential Oils, Hand mixed, poured and cut into long lasting bars",
                    UnitPrice = 12,
                    UnitQuantity = 100,
                    URL = "https://app.imgforce.com/images/user/9j2_1611618012_soap-3.jpg",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    CreatedBy = "Viktor"
                }
            };

            libraryDbContext.Soaps.AddRange(soaps);
            await libraryDbContext.SaveChangesAsync();
        }


        private static async Task SeedOilsAsync(LibraryDbContext libraryDbContext)
        {
            if (libraryDbContext.Oils.Any())
                return;

            var oils = new List<Oil>()
            {
                new Oil()
                {
                    Scent = "Forest Woods",
                    LiquidVolume = 30,
                    Brand = "Hawkins&Brimble",
                    Description = "Perfect for sensitive skin, Le Labo’s formula is entirely " +
                                  "plant-based (making it suitable for vegans). " +
                                  "The nonirritating elixir carries the comforting scent of lavender, " +
                                  "along with bergamot and tonka bean.",
                    UnitPrice = 23,
                    UnitQuantity = 100,
                    URL = "https://app.imgforce.com/images/user/Lb7_1611618063_oil-3.jpg",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    CreatedBy = "Viktor"
                },

                new Oil()
                {
                    Scent = "Cedar",
                    LiquidVolume = 50,
                    Brand = "Johnny's Boat",
                    Description = "Our beard oil comes equipped with a nifty ball rod " +
                                  "cap so that you can take a little or a lot depending on your " +
                                  "beard size. Just add a few drops to your fingertips and massage the " +
                                   "oils through your beard and mustache hair",
                    UnitPrice = 28,
                    UnitQuantity = 100,
                    URL = "https://app.imgforce.com/images/user/ioy_1611618063_oil-5.jpg",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    CreatedBy = "Viktor"
                },

                new Oil()
                {
                    Scent = "Summer Breeze",
                    LiquidVolume = 50,
                    Brand = "Beardo",
                    Description = "Handmade with 100% natural oils, this beard oil " +
                                   "absorbs quickly and gives your beard a smooth, soft, subtle shine. " +
                                   "Great for conditioning the skin underneath your beard. " +
                                   "Promotes hair growth by helping you maintain a healthy beard. " +
                                   "Timber - Mild Scent of Cedar & Fir NeedleMB BEARD OIL TIMBER",
                    UnitPrice = 25,
                    UnitQuantity = 100,
                    URL = "https://app.imgforce.com/images/user/XHI_1611618063_oil-1.jpg",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    CreatedBy = "Viktor"
                }
            };

            libraryDbContext.Oils.AddRange(oils);
            await libraryDbContext.SaveChangesAsync();
        }

        private static async Task SeedBalmsAsync(LibraryDbContext libraryDbContext)
        {
            if (libraryDbContext.Balms.Any())
                return;

            var balms = new List<Balm>()
            {
                new Balm()
                {
                    Volume = 70,
                    Brand = "Honest Amish",
                    Description = "The best for your beard we guarantee it. " +
                                   "Honest Amish Beard Balm is created from the finest organic " +
                                   "ingredients available. We start with a proprietary blend of hair " +
                                   "strengthening botanical infused in a base of Virgin Argan, Avocado, " +
                                   "Almond, Virgin Pumpkin Seed, and Apricot Kernel Oils",
                    UnitPrice = 21,
                    UnitQuantity = 100,
                    URL = "https://app.imgforce.com/images/user/Un5_1611618089_balm-1.jpg",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    CreatedBy = "Viktor"
                },

                new Balm ()
                {
                    Volume = 50,
                    Brand = "Rocky Mountain",
                    Description = "The natural, organic ingredients get deep into the hair and nourishes " +
                                  "it from inside and out! It works on long and short beards to condition & soften " +
                                  "your hair, improve strength & shine",
                    UnitPrice = 19,
                    UnitQuantity = 100,
                    URL = "https://app.imgforce.com/images/user/PoU_1611618089_balm-4.jpg",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    CreatedBy = "Viktor"
                },

                new Balm()
                {
                    Volume = 100,
                    Brand = "Olympus",
                    Description = "All Natural Ingredients - Featuring over eight essential oils, " +
                                  "we are proud to say that our product is all-natural with ingredients that will " +
                                  "leave your beard feeling and looking amazing",
                    UnitPrice = 20,
                    UnitQuantity = 100,
                    URL = "https://app.imgforce.com/images/user/SLw_1611618089_balm-5.jpg",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    CreatedBy = "Viktor"
                }
            };

            libraryDbContext.Balms.AddRange(balms);
            await libraryDbContext.SaveChangesAsync();
        }
    }


}
