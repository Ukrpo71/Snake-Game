mergeInto(LibraryManager.library, {

  SaveExtern: function (data) {
    var dataString = UTF8ToString(data);
    var myObj = JSON.parse(dataString);
    player.setData(myObj);
  },

  LoadExtern: function () {
    player.getData().then(_data=> {
      const myJSON = JSON.stringify(_data);
      myGameInstance.SendMessage('DataPersist', 'LoadYandexData',myJSON);
    })
  },

  GetLang: function () {
    var returnStr = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  GetMode: function () {
    var returnStr = player.getMode();
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  ShowAdv: function(){
  	ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
        	console.log("---------close-----------");
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
    }
})
  },

});