mergeInto(LibraryManager.library, {

  ShowFullScreenReclm: function()
    {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
            onClose: function(wasShown) {
              // some action after close
          },
            onError: function(error) {
              // some action on error
          }
      }
    })
    },

ShowRewardedVideoInGame: function()
    {
		ysdk.adv.showRewardedVideo({
          callbacks: {
          onOpen: () => {
            console.log('Video ad open.');
        },
          onRewarded: () => {
            console.log('Rewarded!');
           // myGameInstance.SendMessage('HintIcon','ShowNextHint');            
        },
          onClose: () => {
            console.log('Video ad closed.');
            myGameInstance.SendMessage('Creator','AddBallToSpawner');          
            myVolumeInstance.SendMessage('SountSettingsManager','MusicPlay');
        }, 
          onError: (e) => {
            console.log('Error while open video ad:', e);
        }
    }
	})
    },

    TestFun: function()
    {
        console.log("it s work otvechav");
    },

    TeFuncrion: function()
    {
        console.log("it s work otvechav");
    },


});