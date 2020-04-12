# create the build instance 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /src                                                                    
COPY ./src ./

# restore solution
RUN dotnet restore NopCommerce.sln

WORKDIR /src/Presentation/Nop.Web   

# build project   
RUN dotnet build Nop.Web.csproj -c Release

# build plugins
WORKDIR /src/Plugins/Nop.Plugin.Product.VideoUploader
RUN dotnet build Nop.Plugin.Product.VideoUploader.csproj -c Release

# publish project
WORKDIR /src/Presentation/Nop.Web   
RUN dotnet publish Nop.Web.csproj -c Release -o /app/published

# create the runtime instance 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime 

# add globalization support
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# installs required packages
RUN apk add libgdiplus --no-cache --repository http://dl-3.alpinelinux.org/alpine/edge/testing/ --allow-untrusted
RUN apk add libc-dev --no-cache

WORKDIR /app        
RUN mkdir bin
RUN mkdir logs  
                                                            
COPY --from=build /app/published .

VOLUME [ "/app/App_Data/dataSettings.json" ]                            

ENTRYPOINT ["dotnet", "Nop.Web.dll"]
