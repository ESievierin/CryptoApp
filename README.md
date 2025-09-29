# CryptoApp

A **.NET (WPF, MVVM)** desktop application for viewing cryptocurrency data using the [CoinGecko API](https://www.coingecko.com/en/api/documentation).

---

## ✨ Features

- Display of **Top 10 cryptocurrencies** by market capitalization  
- Detailed view for each coin:  
  - Current price, market cap, fully diluted valuation, trading volume  
  - 24h price change percentage  
  - 24h minimum and maximum price  
  - Available markets with trading pairs, price, and trade link  
- **Price history chart** (powered by ScottPlot)  
- **Search coins** by name or ticker  
- **Currency converter** with live rates  
- Support for **light / dark theme**  
- Support for **multiple languages**
- Support for **multiple currencies**

---

## 🚀 Technologies

- **.NET 8 / WPF**  
- **MVVM (CommunityToolkit.Mvvm)**  
- **HttpClient** for API communication  
- **ScottPlot** for charting  

---

## ⚙️ Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/CryptoApp.git
   cd CryptoApp
2. Create a file appsettings-local.json in the root directory and add your CoinGecko demo API key:
```code
{
  "CoinGecko": {
    "ApiKey": "your-api-key"
  }
}
