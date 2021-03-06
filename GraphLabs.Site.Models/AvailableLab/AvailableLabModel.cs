using GraphLabs.DomainModel;
using GraphLabs.Site.Models.Infrastructure;

namespace GraphLabs.Site.Models.AvailableLab
{
    /// <summary> ����������� ������ ��, ��������� � ���������� ��������� </summary>
    public abstract class AvailableLabModel : IEntityBasedModel<AbstractLabSchedule>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}