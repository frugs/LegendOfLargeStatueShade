
namespace Assets.Scripts.Room {
    public interface IRoomFactory {
        IRoom CreateStartingRoom();

        IRoom CreateAdjacentRoom(IDoor door);
    }
}
