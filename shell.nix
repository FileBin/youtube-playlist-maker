with (import <nixpkgs> {  });
mkShell {
    buildInputs =
    [
       dotnetCorePackages.sdk_9_0-bin
    ];

    packages = [
        yt-dlp
        nixfmt
        (vscode-with-extensions.override {
            vscodeExtensions = with vscode-extensions; [
                ms-dotnettools.csharp
                ms-dotnettools.csdevkit
                ms-dotnettools.vscode-dotnet-runtime
                ms-vscode.hexeditor
                tal7aouy.icons
                jnoortheen.nix-ide
                redhat.vscode-xml
                wmaurer.change-case
                continue.continue
            ];
        })
    ];
}
