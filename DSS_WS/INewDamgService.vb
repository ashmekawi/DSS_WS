Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "INewDamgService" in both code and config file together.
<ServiceContract()>
Public Interface INewDamgService

    <OperationContract()>
    Function DamgData(UserID As Integer) As DamgDataResponse


End Interface
