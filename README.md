# Distinct Song Counter

This project is the solution to the **PiWorks Technical Assignment (Exhibit A)**.  
The application reads the input dataset, processes listening records, and generates the required output in an optimized way.

---

## Dataset

The input file is **not included** in this repository.

Please download it from:  
https://pi.works/questionnaire-a

After downloading, rename the file to:


Then place it in the **root directory** of the project.

---

## How It Works

- The program reads the `exhibit-a.txt` dataset.
- Distinct songs per user are counted for the target date (`2016-08-10`).
- The final result is written to:


The solution is optimized for speed and large-file processing.

---

## How To Run

1. Add `exhibit-a.txt` to the project root.
2. Build the project:
3. Run the program:

The output file (`output.csv`) will be generated in the project directory.

---

## Project Structure

README.md
distinct-song-counter.csproj
program.cs
output.csv (generated after running)
exhibit-a.txt (must be manually added)
