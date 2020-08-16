set DOCKER_REGISTRY=%1
set VERSION=%2

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% auth .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% battle .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% gateway .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% masterdata .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% notification .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% stage .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% stats .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% inventory .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% forge .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% shop .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% stats .
if ERRORLEVEL 1 GOTO END

call build_server_windows_docker.bat %DOCKER_REGISTRY% %VERSION% admin admin
if ERRORLEVEL 1 GOTO END

:END