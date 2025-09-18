{
	description = "General-purpose .NET development flake";

	inputs = {
		nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
		flake-utils.url = "github:numtide/flake-utils";
	};

	outputs = { self, nixpkgs, flake-utils }:
		flake-utils.lib.eachDefaultSystem (system:
			let
				pkgs = import nixpkgs { inherit system; };
			in {
				devShells.default = pkgs.mkShell {
					buildInputs = [
						pkgs.dotnet-sdk_9
						pkgs.dotnet-runtime
					];
					shellHook = ''
						echo "Welcome to the .NET development shell!"
						dotnet --version
                        export PS1='(nix-shell)[\u@\h \W]\$ '
					'';
				};
			}
		);
}
