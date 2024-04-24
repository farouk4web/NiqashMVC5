$(document).ready(function () {
    var currentUserId = $("#userId").text(),
        ajaxUrlRequest = "/Api/Following";

    if ($(".posts").attr("data-posts-type") === "globe") {
        ajaxUrlRequest = "/Api/Posts";
    }

    function activateLikesAndLoves(post) {
        post.likes.forEach(function (like) {

            if (like.userId === currentUserId) {
                $(".posts .post[data-post-id='" + like.postId + "']").find(".js-like")
                    .attr("data-like-id", like.id)
                    .addClass("activeLike")
                    .addClass("js-unLike")
                    .removeClass("js-like");
            }

        });
        post.loves.forEach(function (love) {

            if (love.userId === currentUserId) {
                $(".posts .post[data-post-id='" + love.postId + "']").find(".js-love")
                    .attr("data-love-id", love.id)
                    .addClass("activeLove")
                    .addClass("js-unLove")
                    .removeClass("js-love");
            }

        });
    }

    function removeZeroFromReactBox() {
        $(".posts .post .reactingNumber").each(function () {
            var reactingNumber = $(this);
            if (reactingNumber.text() === "0") {
                reactingNumber.css("color", "var(--postBg)");
            } else {
                reactingNumber.css("color", "#fff");
            }
        });
    }

    function addDirectionToPosts() {
        //$(".posts .post .postContent").each(function () {
        //    var postContent = $(this).text();
        //    if (postContent.charCodeAt(0) > 200) {
        //        $(this).css("direction", "rtl");
        //    } else {
        //        $(this).css("direction", "ltr");
        //    }
        //});
    }

    function removePostActionsBtns() {
        $('.posts .post').each(function () {
            var postUserId = $(this).attr("data-user-id");

            if (postUserId !== currentUserId) {
                $(this).find(".postActions").empty();
            }
        });
    }

    // load page data from our API
    $.ajax({
        url: ajaxUrlRequest,    
        type: "GET",
        dataType: "json",
        success: function (data) {

            if (data.length === 0) {
                $("#noPostsMsg").fadeIn();
            } else {
                data.forEach(function (post) {
                    // DO NOT FORGET TO CHANGE postData var in create post
                    //var postData = "<div class='post' data-post-id='" + post.id + "' data-user-id='" + post.userId + "'><div class='overflow'><div class='userImageContainer'><img src=' " + post.user.profilePicSrc + "' class='postUserImage img-circle' /></div><div class='postDataAndActions'><div class='overflow'><div class='postData'><h5><a href='/" + post.userId + "'> " + post.user.firstName + " " + post.user.lastName + "</a></h5> <h6>" + post.publishDate + "</h6></div><div class='postActions'><i class='glyphicon glyphicon-trash' id='removePost' data-post-id=" + post.id + "></i><i class='glyphicon glyphicon-edit' id='editPost' data-post-id=" + post.id + "></i></div></div></div></div><p class='postContent lead'>" + post.content + "</p> <div class='reactBox text-center'><div class='reactBtn js-like'><i class='glyphicon glyphicon-thumbs-up'></i><span class='reactingNumber'>" + post.likes.length + "</span></div><div class='reactBtn js-love'><i class='glyphicon glyphicon-heart'></i><span class='reactingNumber'>" + post.loves.length + "</span></div><div class='reactBtn js-comment' data-comments='false''><i class='glyphicon glyphicon-comment'></i><span class='reactingNumber'>" + post.comments.length + "</span></div></div><div class='comments'></div></div>";
                    var postData = `
                                <div class='post' data-post-id='${post.id}' data-user-id='${post.userId}'>
                                    <div class='overflow'>
                                        <div class='userImageContainer'>
                                            <img src='${post.user.profilePicSrc}' class='postUserImage img-circle' />
                                        </div>
                                        <div class='postDataAndActions'>
                                            <div class='overflow'>
                                                <div class='postData'>
                                                    <h5><a href='/users/account/${post.userId}'>${post.user.firstName} ${post.user.lastName}</a></h5>
                                                    <h6>${post.publishDate}</h6>
                                                </div>
                                                <div class='postActions'>
                                                    <i class='glyphicon glyphicon-trash' id='removePost' data-post-id='${post.id}'></i>
                                                    <i class='glyphicon glyphicon-edit' id='editPost' data-post-id='${post.id}'></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <p class='postContent lead'>${post.content}</p>
                                    <div class='reactBox text-center'>
                                        <div class='reactBtn js-like'>
                                            <i class='glyphicon glyphicon-thumbs-up'></i>
                                            <span class='reactingNumber'>${post.likes.length}</span>
                                        </div>
                                        <div class='reactBtn js-love'>
                                            <i class='glyphicon glyphicon-heart'></i>
                                            <span class='reactingNumber'>${post.loves.length}</span>
                                        </div>
                                        <div class='reactBtn js-comment' data-comments='false' '>
                                            <i class='glyphicon glyphicon-comment'></i>
                                            <span class='reactingNumber'>${post.comments.length}</span>
                                        </div>
                                    </div>
                                    <div class='comments'>
                                    </div>
                                </div>
                            `;

                    $(".posts").append(postData);
                    activateLikesAndLoves(post);
                    removePostActionsBtns();
                    removeZeroFromReactBox();
                    addDirectionToPosts();
                });
            }

        },
        error: function () {
            $("#noPostsMsg").fadeIn();
        }
    });

});
