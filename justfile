program-name := 'CheckPath'
project-file := program-name / program-name + '.csproj'
publish-folder := '.' / program-name / 'bin/Release/net10.0/publish'
deploy-folder := if os() == 'linux' { '~/bin' } else { '~/AppData/Local/Programs' } # env('USERPROGRAMS') }

# Run the program with optional args
run *ARGS:
    dotnet run --project {{ project-file }} -- {{ARGS}}

# Build the binary
build:
    dotnet publish -c Release

# Build and deploy the binary
deploy: build
    cp {{ publish-folder }}/* {{ deploy-folder }}

# Remove generated files
clean:
    dotnet clean
