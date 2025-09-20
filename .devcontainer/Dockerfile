# Dev container Dockerfile for .NET 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0

# Install any additional dependencies here
# RUN apt-get update && apt-get install -y <your-packages>

# Set up a non-root user (optional, for security)
# ARG USERNAME=vscode
# ARG USER_UID=1000
# ARG USER_GID=$USER_UID
# RUN groupadd --gid $USER_GID $USERNAME \
#     && useradd --uid $USER_UID --gid $USER_GID -m $USERNAME \
#     && apt-get update \
#     && apt-get install -y sudo \
#     && echo $USERNAME ALL=\(root\) NOPASSWD:ALL > /etc/sudoers.d/$USERNAME \
#     && chmod 0440 /etc/sudoers.d/$USERNAME

WORKDIR /workspaces/SharpTheoryRazorPages
