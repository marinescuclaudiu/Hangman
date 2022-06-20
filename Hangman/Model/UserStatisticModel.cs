using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Hangman.Model
{
    class UserStatisticModel
    {
        public UserStatisticModel(string username)
        {
            Username = username;
        }

        [JsonConstructor]
        public UserStatisticModel(string username, int gamesPlayed, int allCategoriesWins, int animalsWins, int carsWins, int countriesWins, int moviesWins)
        {
            Username = username;
            GamesPlayed = gamesPlayed;
            AllCategoriesWins = allCategoriesWins;
            AnimalsWins = animalsWins;
            CarsWins = carsWins;
            CountriesWins = countriesWins;
            MoviesWins = moviesWins;
        }
    
        public string Username { get; set; }

        public int GamesPlayed { get; set; }

        public int AllCategoriesWins { get; set; }

        public int AnimalsWins { get; set; }

        public int CarsWins { get; set; }

        public int CountriesWins { get; set; }

        public int MoviesWins { get; set; }

    }
}
