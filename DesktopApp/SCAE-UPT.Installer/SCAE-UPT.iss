; Script de Inno Setup para el instalador de SCAE-UPT

[Setup]
AppName=SCAE-UPT
AppVersion=1.0
DefaultDirName={pf}\SCAE-UPT
DefaultGroupName=SCAE-UPT
OutputDir=Output
OutputBaseFilename=SCAE-UPT-Setup
Compression=lzma
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Files]
Source: "..\SCAE-UPT-QR\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\SCAE-UPT"; Filename: "{app}\SCAE-UPT.exe"
Name: "{commondesktop}\SCAE-UPT"; Filename: "{app}\SCAE-UPT.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Crear icono en el escritorio"; GroupDescription: "Tareas adicionales"; Flags: checkedonce

[Run]
Filename: "{app}\SCAE-UPT.exe"; Description: "Iniciar SCAE-UPT"; Flags: nowait postinstall skipifsilent
