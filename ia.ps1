# Navega a la raíz del proyecto antes de ejecutar esto
# Ejemplo: cd 'C:\Users\pc\source\repos\AxioMath'

# Eliminar carpetas bin y obj (recursivamente)
Get-ChildItem -Recurse -Directory -Force | Where-Object { $_.Name -in @('bin', 'obj') } | Remove-Item -Recurse -Force

# Ruta del archivo de salida
$outputFile = "ContextoAxioMath.txt"

# Borrar archivo anterior si existe
if (Test-Path $outputFile) {
    Remove-Item $outputFile
}

# Agregar encabezado
"--- CONTEXTO DE AXIOMATH ---`n" | Out-File -Encoding UTF8 $outputFile

# Incluir todos los .cs, .csproj y .sln ordenados por ruta
Get-ChildItem -Recurse -Include *.cs,*.csproj,*.sln | Sort-Object FullName | ForEach-Object {
    "`n--- FILE: $($_.FullName) ---`n" | Out-File -Append -Encoding UTF8 $outputFile
    Get-Content $_.FullName | Out-File -Append -Encoding UTF8 $outputFile
}
