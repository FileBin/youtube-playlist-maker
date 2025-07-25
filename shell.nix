pkgs.mkShell {
  name = "net-app-shell";
  buildInputs = [
    # Add your .NET dependencies here, e.g.
    # pkgs.dotnet-sdk-6.0
    pkgs.dotnet-core-sdk-3.1
  ];
}