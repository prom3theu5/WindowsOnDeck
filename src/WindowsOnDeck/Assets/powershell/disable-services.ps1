$ErrorActionPreference = "silentlycontinue"

$services = @(
    "DiagTrack"                                
	"fxssvc.exe"				               
    "AxInstSV"								   
    "PcaSvc"                                   
	"dmwappushservice"						    
    "Remote Registry"                           
    "WMPNetworkSvc"                            
    "StiSvc"                                   
    "ndu"                                      
	"wisvc"								        
)	

foreach ($service in $services) {
    try {
        Write-Output "Trying to disable $service"
        Get-Service -Name $service | Set-Service -StartupType Disabled
        
    }
    catch { }
}