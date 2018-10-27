
var camelCaseTokenizer = function (obj) {
    var previous = '';
    return obj.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
        var current = cur.toLowerCase();
        if(acc.length === 0) {
            previous = current;
            return acc.concat(current);
        }
        previous = previous.concat(current);
        return acc.concat([current, previous]);
    }, []);
}
lunr.tokenizer.registerFunction(camelCaseTokenizer, 'camelCaseTokenizer')
var searchModule = function() {
    var idMap = [];
    function y(e) { 
        idMap.push(e); 
    }
    var idx = lunr(function() {
        this.field('title', { boost: 10 });
        this.field('content');
        this.field('description', { boost: 5 });
        this.field('tags', { boost: 50 });
        this.ref('id');
        this.tokenizer(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
    });
    function a(e) { 
        idx.add(e); 
    }

    a({
        id:0,
        title:"DiscordChatMessageResult",
        content:"DiscordChatMessageResult",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"DiscordProvider",
        content:"DiscordProvider",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"DiscordAliases",
        content:"DiscordAliases",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"DiscordChatProvider",
        content:"DiscordChatProvider",
        description:'',
        tags:''
    });

    a({
        id:4,
        title:"DiscordChatMessageSettings",
        content:"DiscordChatMessageSettings",
        description:'',
        tags:''
    });

    y({
        url:'/Cake.Discord/api/Cake.Discord.Chat/DiscordChatMessageResult',
        title:"DiscordChatMessageResult",
        description:""
    });

    y({
        url:'/Cake.Discord/api/Cake.Discord/DiscordProvider',
        title:"DiscordProvider",
        description:""
    });

    y({
        url:'/Cake.Discord/api/Cake.Discord/DiscordAliases',
        title:"DiscordAliases",
        description:""
    });

    y({
        url:'/Cake.Discord/api/Cake.Discord.Chat/DiscordChatProvider',
        title:"DiscordChatProvider",
        description:""
    });

    y({
        url:'/Cake.Discord/api/Cake.Discord.Chat/DiscordChatMessageSettings',
        title:"DiscordChatMessageSettings",
        description:""
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
