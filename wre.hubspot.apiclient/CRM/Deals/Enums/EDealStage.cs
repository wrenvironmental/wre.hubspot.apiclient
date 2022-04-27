using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.CRM.Deals.Enums;

public enum EDealStage
{
    AppointmentScheduled,
    QualifiedToBuy,
    PresentationScheduled,
    DecisionMakerBoughtIn,
    ContractSent,
    ClosedWon,
    ClosedLost
}