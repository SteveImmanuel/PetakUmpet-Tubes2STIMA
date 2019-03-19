/////////////////////////////////////////////////////////////
///                    Tugas Besar II                     ///
///              IF2211 : Strategi Algoritma              ///
/////////////////////////////////////////////////////////////

Petak Umpet dengan Memanfaatkan Penelusuran pada Graf
(Penerapan DFS dan Backtracking)

File : 
- Graph.cs
- MainGUI.cs
- ReadFile.cs
- Algorithm.cs
- Form1.cs 

Bahasa Pemrograman : C#
Namespace yang digunakan : Tubes2Stima

Syarat dan langkah-langkah kompilasi :
- Memiliki Visual Studio dengan kakas .NET dan MSAGL terinstalasi.
- Program dijalankan pada sistem operasi Windows.
- Ambil file map dan file command dengan tombol Choose file, lalu Load file
  tersebut ke GUI
- Tekan tombol Solve untuk Menggambar graf dan mencetak hasil dari setiap
  pertanyaan Ferdiant.
- Setelah GUI kedua terbuka, terdapat graf peta dan list pertanyaan Ferdiant dari
  file command. Tekan salah satu pertanyaan dan tekan tombol Show Path. Atau tambahkan
  pertanyaan custom anda sendiri langsung pada bagian Query (input).
- Jika jawaban adalah 'YA', maka akan tertampil jalur pada graf yang tertampil.
- Jika jawaban adalah 'TIDAK', akan terbuka error dialog yang menyatakan bahwa
  tidak ada jalur yang ditemukan.
  

Fitur-fitur : 
- Mengambil data peta dan pertanyaan dari File eksternal dengan
  ekstensi .txt.
- Pengambilan data menggunakan Open File Dialog.
- Graf yang tergambar menggunakan algoritma Force Directed Graph hingga mendapat
  bentuk graf yang stabil.
- Front end terhubung dengan backend untuk mendapatkan jawaban atas semua
  pertanyaan Ferdiant.
- Pertanyaan Ferdiant secara default diambil dari file eksternal, akan tetapi program
  menyediakan input box pada GUI untuk membuat pertanyaan baru.

Informasi-informasi tambahan : 
- Visualisasi graf menggunakan MSAGL (Microsoft Automatic Graph Layout).

Dibuat Oleh :
Aditya Putra Santosa   / 13517013
Steve Andreas Immanuel / 13517039
Leonardo               / 13517048

Program Studi Teknik Informatika
Sekolah Teknik Elektro dan Informatika
Institut Teknologi Bandung
2019