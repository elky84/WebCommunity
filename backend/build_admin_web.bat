rmdir "../service/nginx/html" /S /Q
mkdir "../service/nginx/html"
rmdir "../service/nginx/logs" /S /Q
mkdir "../service/nginx/logs"
rmdir "../service/nginx/temp" /S /Q
mkdir "../service/nginx/temp"

cd Admin

call npm install

call npm run build -- --mode local

robocopy "./dist" "../../service/nginx/html" "*.*" /E

cd ..