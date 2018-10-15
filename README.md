# iksm_refresher
This app make a GET request to `https://app.splatoon2.nintendo.net/home` every hour to keep your `iksm_session` cookie staaaay fresh!

## Usage
1. Get your cookie from smartphone app and set it to `config.json`  
   (You can use the [mitmproxy instructions](https://github.com/frozenpandaman/splatnet2statink/wiki/mitmproxy-instructions))
1. Run ```iksm_refresher.exe```

### Install as a service
Run ```iksm_refresher.exe install``` in the privileged prompt.  
(```iksm_refresher.exe uninstall``` to uninstall.)
