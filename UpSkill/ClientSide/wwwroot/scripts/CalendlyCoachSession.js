

function isCalendlyEvent(e) {
    return e.data.event &&
        e.data.event.indexOf('calendly') === 0;
};

window.addEventListener(
    'message',
    function (e) {
        if (isCalendlyEvent(e)) {
            if (e.data['event'] == 'calendly.event_scheduled') {


                let data = {
                    eventUri: e.data.payload.event.uri,
                    inviteeUri: e.data.payload.invitee.uri
                };

               
                //fetch("https://upskillapi.azurewebsites.net/CoachSessions/AddSession", {
                fetch("https://localhost:5001/CoachSessions/AddSession", {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(data)
                }).then(res => {

                    //TODO Add Callback from JS Event listener to Blazor 
                      //if (res.status == 200) {
                      //  DotNet.invokeMethodAsync('UpSkill.ClientSide', 'AddedSession');
                    //}

                });
            }
        }
    }
);