﻿using System;
using System.Linq;
using System.Collections.Generic;
using Bct.SearchFight.Shared;
using Bct.SearchFight.Core.Models;
using Bct.SearchFight.Core.Interfaces;

namespace Bct.SearchFight.Core.Impl
{
    public class WinnerManager : IWinnerManager
    {
        public SearchEngineWinner GetGrandWinner(IList<Search> searchData)
        {
            if (searchData == null || searchData.Count() == 0)
                throw new ArgumentException("The specified argument is invalid.", nameof(searchData));

            Search searchWinner = searchData.GetMax(item => item.Results);
            return new SearchEngineWinner() { Engine = searchWinner.SearchEngine, Term = searchWinner.Term };
        }

        public IEnumerable<SearchEngineWinner> GetSearchEngineWinners(IList<Search> searchData)
        {
            if (searchData == null || searchData.Count() == 0)
                throw new ArgumentException("The specified argument is invalid.", nameof(searchData));

            IEnumerable<SearchEngineWinner> searchEngines = searchData.GroupBy(data => data.SearchEngine,
                result => result, (searchEngine, results) => new SearchEngineWinner
                {
                    Engine = searchEngine,
                    Term = results.GetMax(item => item.Results).Term
                });

            return searchEngines;
        }
    }
}
