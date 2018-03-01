using System;
using System.Collections.Generic;

using CurrencyConverter.Models;

namespace CurrencyConverter.Api
{
    public interface ICoinApi
    {
        T GetData<T>(string url);

        List<Exchange> MetadataListExchanges();

        List<Asset> MetadataListAssets();
        List<Symbol> MetadataListSymbols();

        Exchangerate ExchangeRatesGetSpecificRate(string baseId, string quoteId, DateTime time);
        Exchangerate ExchangeRatesGetSpecificRate(string baseId, string quoteId);

        ExchangeCurrentrate ExchangeRatesGetAllCurrentRates(string baseId);

        List<Period> OhlcvListAllPeriods();

        List<OHLCV> OhlcvLatestData(string symbolId, string periodId, int limit);
        List<OHLCV> OhlcvLatestData(string symbolId, string periodId);

        List<OHLCV> OhlcvHistoricalData(string symbolId, string periodId, DateTime start, DateTime end, int limit);
        List<OHLCV> OhlcvHistoricalData(string symbolId, string periodId, DateTime start, DateTime end);
        List<OHLCV> OhlcvHistoricalData(string symbolId, string periodId, DateTime start, int limit);
        List<OHLCV> OhlcvHistoricalData(string symbolId, string periodId, DateTime start);

        List<Trade> TradesLatestDataAll();
        List<Trade> TradesLatestDataAll(int limit);

        List<Trade> TradesLatestDataSymbol(string symbolId);
        List<Trade> TradesLatestDataSymbol(string symbolId, int limit);

        List<Trade> TradesHistoricalData(string symbolId, DateTime start, DateTime end, int limit);
        List<Trade> TradesHistoricalData(string symbolId, DateTime start);
        List<Trade> TradesHistoricalData(string symbolId, DateTime start, DateTime end);
        List<Trade> TradesHistoricalData(string symbolId, DateTime start, int limit);


        Quote QuotesCurrentDataSymbol(string symbolId);

        List<Quote> QuotesCurrentDataAll();
        List<Quote> QuotesLatestDataAll();
        List<Quote> QuotesLatestDataAll(int limit);

        List<Quote> QuotesLatestDataSymbol(string symbolId);
        List<Quote> QuotesLatestDataSymbol(string symbolId, int limit);

        List<Quote> QuotesHistoricalData(string symbolId, DateTime start, DateTime end, int limit);
        List<Quote> QuotesHistoricalData(string symbolId, DateTime start);
        List<Quote> QuotesHistoricalData(string symbolId, DateTime start, DateTime end);
        List<Quote> QuotesHistoricalData(string symbolId, DateTime start, int limit);

        Orderbook OrderbooksCurrentDataSymbol(string symbolId);

        List<Orderbook> OrderbooksCurrentDataAll();

        List<Orderbook> OrderbooksLastData(string symbolId, int limit);
        List<Orderbook> OrderbooksLastData(string symbolId);

        List<Orderbook> OrderbooksHistoricalData(string symbolId, DateTime start, DateTime end, int limit);
        List<Orderbook> OrderbooksHistoricalData(string symbolId, DateTime start);
        List<Orderbook> OrderbooksHistoricalData(string symbolId, DateTime start, DateTime end);
        List<Orderbook> OrderbooksHistoricalData(string symbolId, DateTime start, int limit);

        List<Twitter> TwitterLastData(int limit);
        List<Twitter> TwitterLastData();

        List<Twitter> TwitterHistoricalData(DateTime start, DateTime end, int limit);
        List<Twitter> TwitterHistoricalData(DateTime start);
        List<Twitter> TwitterHistoricalData(DateTime start, DateTime end);
        List<Twitter> TwitterHistoricalData(DateTime start, int limit);
    }
}
