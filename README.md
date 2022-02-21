# simpleapi
Azure Devops Example: deploying a webapi template for future reference
# Azure Devops: Continuous Deployment (Maunally Version)
Assumption(s)
* A functional .NET Core webapi application committed to GitHub 
* A functional .NET Core webapi with an Xunit project committed to GitHub
Access ###Azure DevOps Portal### create a new project and follow the required steps to create a new project. 
Afterwards, go to ###Azure DevOps Portal### > ###PipeLines, create a new pipline follow and select Github as your codebase repo. 
For this project, follow the authentication and authroizations steps for Azure to access your Github account. 
The following page, Azure will provide a list of all the repos under your name. Select your project. 
Final step, you can select template for your azure-pipeline.yaml file. For this project, I selected ASP.NET as the template. 

```yaml
trigger:
- main

pool:
  vmImage: 'windows-latest'

variables:
  solution: '**/*.sln'
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'

steps:
- task: NuGetToolInstaller@1

- task: NuGetCommand@2
  inputs:
    restoreSolution: '$(solution)'

- task: VSBuild@1
  inputs:
    solution: '$(solution)'
    msbuildArgs: '/p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:SkipInvalidConfigurations=true /p:PackageLocation="$(build.artifactStagingDirectory)"'
    platform: '$(buildPlatform)'
    configuration: '$(buildConfiguration)'

- task: VSTest@2
  inputs:
    platform: '$(buildPlatform)'
    configuration: '$(buildConfiguration)'
```

Azure Devops will run the configured pipeline, and commit the azure-pipeline file to your repo. 
If successful, then we add our xunit testing to the pipeline. Follow the instruction on this site [https://docs.microsoft.com/en-us/azure/devops/pipelines/ecosystems/dotnet-core?view=azure-devops&tabs=dotnetfive]


