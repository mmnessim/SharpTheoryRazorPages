# SharpTheory 
SharpTheory is an API with a demo webapp for basic music theory topics. It is written in C# using ASP.NET Core Razor Pages. 

## Features 
- **JSON Data:**
  Raw music theory data about keys, diatonic/nondiatonic scales, intervals, stored in JSON
- **Public API:**
  REST endpoints for other developers to use in their own music related projects
- **Bootstrap UI**
- **Key Reference**
- **Key Quiz**

## Demo
This project is available hosted out of my homelab at https://theory.mnessim.com 

## Getting Started
1. **Clone this repository**
  ```bash
     git clone https://github.com/mmnessim/SharpTheoryRazorPages.git
     cd SharpTheoryRazorPages/SharpTheory
   ```
2. **Run directly**
```bash
    dotnet run --project SharpTheory
```
3. **Optional: Run using Docker Compose**
   ```bash
   docker compose up -d
   ```

  
## Technologies Used

- C#
- ASP.NET Core Razor Pages
- ASP.NET Identity
- Bootstrap (UI)
- Swagger (API docs)
- Docker, Nix (dev/deploy, optional)

## License

MIT License

---
**Author:** [mmnessim](https://github.com/mmnessim)  
See also the [music_theory_json](https://github.com/mmnessim/music_theory_json) repo for raw data.
