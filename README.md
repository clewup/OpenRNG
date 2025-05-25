# 🎲 OpenRNG

[![build](https://img.shields.io/github/actions/workflow/status/clewup/OpenRNG/main_openrngapi.yml?branch=main)](https://github.com/clewup/OpenRNG/actions)
[![coverage](https://coveralls.io/repos/github/clewup/OpenRNG/badge.svg?branch=main)](https://coveralls.io/github/clewup/OpenRNG?branch=main)
![license](https://img.shields.io/badge/license-MIT-green)
![NuGet](https://img.shields.io/nuget/v/OpenRNG.Core)

> OpenRNG is a versatile and open-source .NET API providing cryptographically secure random data generation and entropy analysis tools. It offers endpoints to generate random integers, lorem ipsum text, avatars, colors, passwords, and entropy metrics — ideal for developers, testers, and security enthusiasts.

---

## 🚀 Features

- Generate cryptographically secure random integers within a range  
- Generate customizable lorem ipsum placeholder text  
- Create deterministic avatar URLs based on seeds  
- Generate random colors in HEX format  
- Create secure random passwords with configurable options  
- Analyze entropy (Shannon, Min, Rényi) of input strings  
- Visualize entropy distribution with frequency data  
- Rate limiting and caching for performance and reliability  

---

## 🌐 Public Access For Developers

The OpenRNG API is designed to be accessible publicly for developers and testers who need secure and reliable random data generation.

- [Try it out with Scalar](https://openrngapi-h8b7hsajbbcxfter.westeurope-01.azurewebsites.net/scalar/v1)
- Base URL: https://openrngapi-h8b7hsajbbcxfter.westeurope-01.azurewebsites.net
- No authentication is required for most endpoints to enable quick and easy access.
- Rate limiting is enforced to ensure fair usage and prevent abuse (default: 100 requests per minute per IP).
- Caching is implemented on key endpoints like lorem text and avatar generation to optimize performance.
- Please respect the rate limits and avoid excessive or abusive usage.

---

## ⚡ Quick Start

### Clone the repo and run

```bash
git clone https://github.com/clewup/OpenRNG.git
cd OpenRNG
dotnet run --project OpenRNG.Api
```

---

## ⚙️ Configuration

The API supports optional rate limiting and caching to improve performance and prevent abuse.

- Rate limiting: Default 100 requests per minute per IP
- Caching: Response caching enabled for frequently requested endpoints (e.g., lorem, avatar)

---

## 🧪 Running Tests

```bash
dotnet test
```

---

## 🤝 Contributing

Contributions are welcome! Please fork the repo and submit pull requests for bug fixes, improvements, or new features.

---

## 📄 License

This project is licensed under the MIT License. See the LICENSE file for details.
