namespace SwimmingApplication;

public enum SwimmingUserEvent{
    AddRegistration    
}

public class SwimmingUserEventArgs
{
     private readonly SwimmingUserEvent userEvent;
        private readonly Object data;
    
        public SwimmingUserEventArgs(SwimmingUserEvent userEvent, object data)
        {
            this.userEvent = userEvent;
            this.data = data;
        }
    
        public SwimmingUserEvent SwimmingUserEvent
        {
            get { return userEvent; }
        }
    
        public object Data
        {
            get { return data; }
        }
}