function isCalendlyEvent(e) {
    return e.data.event &&
        e.data.event.indexOf('calendly') === 0;
};

window.addEventListener(
    'message',
    function (e) {
        if (isCalendlyEvent(e)) {
            if (e.data['event'] == 'calendly.event_scheduled') {


                //console.log(e)
                //console.log(e.currentTarget.getAttribute('data-dotnetobject'))
                //console.log(dotnetreference)
                //console.log('After reference')


                let data = {
                    eventUri: e.data.payload.event.uri,
                    inviteeUri: e.data.payload.invitee.uri
                };


                fetch("https://localhost:5001/CoachSessions/AddSession", {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(data)
                }).then(res => {

                      if (res.status == 200) {
                        DotNet.invokeMethodAsync('UpSkill.ClientSide', 'AddedSession');
                    }

                });
            }
        }
    }
);