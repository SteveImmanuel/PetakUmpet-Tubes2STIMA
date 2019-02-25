/////////////////////////////////////////////////////////////
///                    Tugas Besar II                     ///
///                IF2123 : Aljabar Geometri              ///
/////////////////////////////////////////////////////////////

Simulasi Transformasi Linier pada Bidang 2D dan 3D dengan Menggunakan OpenGL API

Files :
1. Source Codes
    - main.py
    - transformasi.py
    - ProcInput.py
    - config.py
    - render.py
    - bentuk.py
    - handler.py
    - animasi.py

2. Documentations
    - LAPORAN TUGAS BESAR II IF2123.pdf
    - README.txt

Language : Python 3

Compiling and Running Requirements :
- Pengguna memiliki Python 3 terinstall di komputernya.
- Pengguna memiliki pip OpenGL, numpy, random, math, queue, dan ctypes.
- lakukan perintah 'python main.py' untuk mengcompile dan run program
- Untuk dapat menjalankan program sesuai spesifikasi, diperlukan 1 window
  GUI untuk melihat bentuk dan ruang secara geometris dan 1 command window
  untuk melakukan transformasi

How to Use
Fungsi-fungsi transformasi yang bisa dilakukan :
(Pastikan untuk melakukan input di command window yang sedang terhubung
dengan window GUI yang ada)
1. 2D
  - input beberapa titik dan menjadikannya bentuk geometris
  - translate <dx> <dy>
  - rotate <deg> <x> <y>
  - dilate <k>
  - reflect <param>
  - shear <param> <k>
  - stretch <param> <k>
  - custom <a> <b> <c> <d>
  - multiple <n>
      input ke-1
      input ke-2
      input ke-3
      .
      .
      .
      input ke-n
  - reset
  - exit
  - w, a, s, d (menggerakan kamera)
  - q, e (zoom in dan zoom out)
2. 3D
  - bentuk yang ditampilkan langsung berupa kubus
  - translate <dx> <dy> <dz>
  - rotate <deg> <x> <y> <z>
  - dilate <k>
  - reflect <param>
  - shear <param> <k>
  - stretch <param> <k>
  - custom <a> <b> <c> <d> <e> <f> <g> <h> <i>
  - multiple <n>
      input ke-1
      input ke-2
      input ke-3
      .
      .
      .
      input ke-n
  - reset
  - exit
  - w, a, s, d (menggerakan kamera)

Additional Informations :
1. Pewarnaan bidang dilakukan dengan random dan setiap
   player melakukan perintah reset, warna bidang akan berubah.
2. Ada bug pada penggunaan OpenGL yang membuat apabila untuk
   pewarnaan bidang menggunakan warna agak transparan, warna
   tidak dapat diconvert kembali menjadi warna solid.

Dibuat Oleh :
Aditya Putra Santosa  / 13517013
Harry Rahmadi Munly   / 13517033
Leonardo              / 13517048

Prodi Teknik Informatika
Sekolah Teknik Elektro dan Informatika
Institut Teknologi Bandung
2018
