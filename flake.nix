{
  description = "dotnet6 devshell";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-24.05"; # DOTNET SDK 6.0 EOL in latest nixpkgs
  };

  outputs = { self, nixpkgs }: {
    devShells.x86_64-linux.default = nixpkgs.legacyPackages.x86_64-linux.mkShell {
      buildInputs = with nixpkgs.legacyPackages.x86_64-linux; [
        dotnet-sdk_6
      ];

      shellHook = ''
        dotnet --version
      '';
    };
  };
}
