
Az online felület telepítésének menete:
1. Backend könyvtárban "composer update" futtatása.
2. Frontend könyvtárban "npm install" futtatása.
3. Backend könyvtárban migráció futtatása "php artisan migrate".
4. Adatbázis-kezelőben query lefuttatása: "Database/menugenius_imagedatas_eng.sql"
5. Backend könytárban "php artisan db:seed" futtatása.


