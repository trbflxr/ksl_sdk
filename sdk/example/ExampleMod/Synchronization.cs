using KSL.API;

namespace Example {
	public class Synchronization {
		private bool isAvailable_;
		private int packetId_;

		public void OnInit() {
			isAvailable_ = Kino.Sync.IsAvailable;
			if (!isAvailable_) {
				Kino.Log.Error("Synchronization is not available");
				return;
			}

			if (!Kino.Sync.TryRegisterPacket("test_name", out packetId_)) {
				Kino.Log.Error("Unable to register test packet");
				return;
			}

			if (!Kino.Sync.SetCallback(packetId_, OnPacketReceived)) {
				Kino.Log.Error("Unable to register callback for test packet");
				return;
			}

			Kino.Log.Info("Synchronization initialized");
		}

		public void Draw() {
			Kino.UI.Label("Example ISync usage");

			if (Kino.UI.Button("Send test packet")) {
				TestSendToAll();
			}
		}

		public void TestSendToAll() {
			if (!isAvailable_) {
				return;
			}

			var packet = Kino.Sync.CreatePacket(packetId_);
			packet.WriteString("Hello?");

			if (!Kino.Sync.SendToAll(packet)) {
				Kino.Log.Error($"Unable to send test packet");
			}
		}

		private void OnPacketReceived(IPacket packet) {
			if (packet.Id == packetId_) {
				string text = packet.ReadString();
				Kino.Log.Info($"Received packet {packetId_}: {text}");
			}
		}
	}
}