using Assets.Scripts.Util;

namespace Assets.Scripts.Room {
    public interface IDoor {
        Id<IDoor> Id { get; }
        Id<IDoor> OpposingDoorId { get; }
    }
}