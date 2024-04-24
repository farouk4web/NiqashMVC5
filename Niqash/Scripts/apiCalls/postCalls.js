$(document).ready(function () {
    var currentUserId = $("#userId").text(),

        postId = 123,
        postElement = "postSelector",
        postContent = "postContent",

        commentId = 456,
        commentElement = "commentSelector",
        commentContent = "commentContent";

    // functions
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
        $(".posts .post .postContent").each(function () {
            //var postContent = $(this).text();
            //if (postContent.charCodeAt(0) > 200) {
            //    $(this).css("direction", "rtl");
            //} else {
            //    $(this).css("direction", "ltr");
            //}
        });
    }

    function removePostActionsBtns() {
        $('.posts .post').each(function () {
            var postUserId = $(this).attr("data-user-id");

            if (postUserId !== currentUserId) {
                $(this).find(".postActions").empty();
            }
        });
    }



    // create new post and append to page customHide
    $("#postForm").on("submit", function (event) {
        event.preventDefault();
        var postContent = $(this).find("textarea"),
            postContentValue = postContent.val(),
            postForm = $(this);

        postForm.find("button").button("loading");

        if (postContent.hasClass("valid")) {
            $.ajax({
                url: "/Api/Posts",
                type: "POST",
                dataType: "Json",
                data: {
                    content: postContentValue
                }, 
                success: function (post) {
                    postForm[0].reset();
                    var postData = `
                                <div class='post customHide' data-post-id='${post.id}' data-user-id='${post.userId}'>
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
                                            <i class='glyphicon glyphicon-heart'></i><span class='reactingNumber'>${post.loves.length}</span>
                                        </div>
                                        <div class='reactBtn js-comment' data-comments='false' '><i class='glyphicon glyphicon-comment'>
                                            </i>
                                            <span class='reactingNumber'>${post.comments.length}</span>
                                        </div>
                                    </div>
                                    <div class='comments'>
                                    </div>
                                </div>
                            `;

                    postForm.find("button").button("reset");
                    postForm.after(postData);
                    postForm.siblings(".post.customHide").fadeIn("slow", function () {
                        $(this).removeClass("customHide");
                    });

                    $("#noPostsMsg").fadeOut();

                    removePostActionsBtns();
                    removeZeroFromReactBox();
                    addDirectionToPosts();
                }
            });
        }

    });




    // update post scenario
    $(".posts").on("click", "#editPost", function () {
        postId = $(this).attr("data-post-id");
        postContent = $(this).parents(".post").find(".postContent").text();

        $('#editPostModal').modal('toggle');
        $('#editPostModal').find("textarea").val(postContent);
    });

    // confirm update post
    $("#confirmPostUpdate").on("submit", function (e) {
        e.preventDefault();

        var submitBtn = $(this).find("button[type='submit']");
        submitBtn.button("loading");

        postContent = $(this).find("textarea").val();
        postElement = $(".post[data-post-id='" + postId + "']");

        // create ajax request
        $.ajax({
            url: "/api/posts/" + postId,
            type: "Put",
            dataType: "json",
            data: {
                content: postContent
            },
            success: function (updatedPost) {
                postElement.find(".postContent").text(updatedPost.content);
                submitBtn.button("reset");
                $('#editPostModal').modal('toggle');
            }
        });
    });




    // remove post scenario
    $(".posts").on("click", "#removePost", function () {
        postId = $(this).attr("data-post-id");
        postElement = $(this).parents(".post");
        $('#removePostModal').modal('toggle');
    });

    // confirm Remove post
    $(".posts").on("click", "#confirmRemovePost", function () {
        var submitBtn = $(this);
        submitBtn.button("loading");

        $.ajax({
            url: "/api/posts/" + postId,
            type: "Delete",
            success: function () {
                submitBtn.button("reset");

                postElement.fadeOut("slow", function () {
                    $('#removePostModal').modal('toggle');
                    postElement.remove();
                });
            }

        });
    });



                    /// comments ///
    // get Comments of post and comment form
    $(".posts").on("click", ".js-comment", function () {

        var commentsSection = $(this).parents(".post").find(".comments");
        var dataComments = $(this).attr("data-comments");

        if (dataComments === "false") {
            $(this).attr("data-comments", "true");

            // clone form code
            var commentForm = $("#commentForm").clone();
            var postId = $(this).parents(".post").attr("data-post-id");

            commentForm
                .removeAttr("id")
                .removeClass("hidden")
                .addClass("commentForm")
                .find("#NewComment_PostId").val(postId);

            // insert comment form into comments section 
            commentsSection.append(commentForm);

            // create ajax request to call all comments
            $.ajax({
                url: "/Api/Posts/" + postId,
                type: "GET",
                dataType: "json",
                success: function (data) {

                    if (data.comments.length !== 0) {
                        data.comments.forEach(function (comment) {
                            //var commentData = "<div class='comment' data-comment-id='" + comment.id + "' data-user-id='" + comment.userId + "'><div class='userImageContainer'><img src='" + comment.user.profilePicSrc + "' class='commentUserImage img-circle' alt='" + comment.user.firstName + "' /></div><div class='commentData'><div class='commentBody'><span><a href='#'>" + comment.user.firstName + " " + comment.user.lastName + "</a></span><p class='commentContent'>" + comment.content + "</p></div><div class='overflow'><div class='publishDate'><time>" + comment.publishDate + "</time></div><div class='commentActions'><i class='glyphicon glyphicon-trash' id='removeComment' data-comment-id='" + comment.id + "'></i><i class='glyphicon glyphicon-edit' id='editComment' data-comment-id='" + comment.id + "'></i></div></div></div></div>";
                            var commentData = `
                                    <div class='comment' data-comment-id='${comment.id}' data-user-id='${comment.userId}'>
                                        <div class='userImageContainer'>
                                            <img src='${comment.user.profilePicSrc}' class='commentUserImage img-circle' alt='${comment.user.firstName}' />
                                        </div>
                                        <div class='commentData'>
                                            <div class='commentBody'>
                                                <span><a href='/users/account/${comment.userId}'>${comment.user.firstName} ${comment.user.lastName}</a></span>
                                                <p class='commentContent'>${comment.content}</p>
                                            </div>
                                            <div class='overflow'>
                                                <div class='publishDate'>
                                                    <time>${comment.publishDate}</time>
                                                </div>
                                                <div class='commentActions'>
                                                    <i class='glyphicon glyphicon-trash' id='removeComment' data-comment-id='${comment.id}'></i>
                                                    <i class='glyphicon glyphicon-edit' id='editComment' data-comment-id='${comment.id}'></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                            `;


                            commentsSection.append(commentData);
                            removeCommentActionsBtns();
                        });
                    } else {
                        var noCommentsMsg = $("#noCommentsMsg").clone().removeAttr("id");
                        commentsSection.append(noCommentsMsg);
                    }

                    commentsSection.slideDown();
                }

            });

        } else if (dataComments === "true") {
            $(this).attr("data-comments", "false");

            commentsSection.slideUp("slow", function () {
                commentsSection.empty();
            });

        }
    });

    // create new comment and append to his post
    $(".posts").on("keyup","#NewComment_Content",function (e) {
        var key = e.which;
        if (key === 13) {
            // As ASCII code for ENTER key is "13"
            $(this).parents(".commentForm").submit();
        }
    });

    $(".posts").on("submit", ".commentForm", function (e) {
        e.preventDefault();
        var commentForm = $(this);

        var commentContent = $(this).find("textarea");
        var commentContentValue = $(this).find("textarea").val();
        var commentPostIdValue = $(this).find("#NewComment_PostId").val();

        commentForm[0].reset();
       // if (commentContent.hasClass("valid")) {

        $.ajax({
            url: "/Api/Comments",
            type: "POST",
            dataType: "json",
            data: {
                content: commentContentValue,
                postId: commentPostIdValue
            },
            success: function (comment) {
                
                //var commentData = "<div class='comment customHide' data-comment-id='" + comment.id + "' data-user-id='" + comment.userId + "'><div class='userImageContainer'><img src='" + comment.user.profilePicSrc + "' class='commentUserImage img-circle' alt='" + comment.user.firstName + "' /></div><div class='commentData'><div class='commentBody'><span><a href='#'>" + comment.user.firstName + " " + comment.user.lastName + "</a></span><p class='commentContent'>" + comment.content + "</p></div><div class='overflow'><div class='publishDate'><time>" + comment.publishDate + "</time></div><div class='commentActions'><i class='glyphicon glyphicon-trash' id='removeComment' data-comment-id='" + comment.id + "'></i><i class='glyphicon glyphicon-edit' id='editComment' data-comment-id='" + comment.id + "'></i></div></div></div></div>";
                var commentData = `
                                    <div class='comment customHide' data-comment-id='${comment.id}' data-user-id='${comment.userId}'>
                                        <div class='userImageContainer'>
                                            <img src='${comment.user.profilePicSrc}' class='commentUserImage img-circle' alt='${comment.user.firstName}' />
                                        </div>
                                        <div class='commentData'>
                                            <div class='commentBody'>
                                                <span><a href='/users/account/${comment.userId}'>${comment.user.firstName} ${comment.user.lastName}</a></span>
                                                <p class='commentContent'>${comment.content}</p>
                                            </div>
                                            <div class='overflow'>
                                                <div class='publishDate'>
                                                    <time>${comment.publishDate}</time>
                                                </div>
                                                <div class='commentActions'>
                                                    <i class='glyphicon glyphicon-trash' id='removeComment' data-comment-id='${comment.id}'></i>
                                                    <i class='glyphicon glyphicon-edit' id='editComment' data-comment-id='${comment.id}'></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                            `;
                commentForm.after(commentData);

                removeCommentActionsBtns();

                commentForm.siblings(".customHide").fadeIn("slow", function () {
                    $(this).removeClass("customHide");

                    commentForm.siblings(".noCommentsMsg").fadeOut("slow", function () {
                        $(this).remove();
                    });
                });
                
                // change reactingNumber
                var reactingNumber = commentForm.parents(".post").find(".js-comment .reactingNumber"),
                    currentReactingNumber = parseInt(reactingNumber.text());

                reactingNumber.text(currentReactingNumber + 1);
                removeZeroFromReactBox();
            }
        });


        //}


    });

    // remove comment actions {delete, update, ....... }
    function removeCommentActionsBtns() {
        $('.posts .post .comment').each(function () {
            var commentUserId = $(this).attr("data-user-id");

            if (commentUserId !== currentUserId) {
                $(this).find(".commentActions").empty();
            }
        });
    }



    // update comment scenario
    $(".posts").on("click", "#editComment", function () {
        commentId = $(this).attr("data-comment-id");
        commentContent = $(this).parents(".comment").find(".commentContent").text();

        $('#editCommentModal').modal('toggle');
        $('#editCommentModal').find("textarea").val(commentContent);
    });

    // confirm update comment
    $("#confirmCommentUpdate").on("submit", function (e) {
        e.preventDefault();

        var submitBtn = $(this).find("button[type='submit']");
        submitBtn.button("loading");

        commentContent = $(this).find("textarea").val();
        commentElement = $(".comment[data-comment-id='" + commentId + "']");

        // create ajax request
        $.ajax({
            url: "/api/comments/" + commentId,
            type: "Put",
            dataType: "json",
            data: {
                content: commentContent
            },
            success: function (updatedComment) {
                commentElement.find(".commentContent").text(updatedComment.content);
                submitBtn.button("reset");
                $('#editCommentModal').modal('toggle');
            }
        });
    });








    // remove comment scenario
    $(".posts").on("click", "#removeComment", function () {
        commentId = $(this).attr("data-comment-id");
        commentElement = $(this).parents(".comment");
        $('#removeCommentModal').modal('toggle');
    });

    // confirm Remove comment
    $(".posts").on("click", "#confirmRemoveComment", function () {
        var submitBtn = $(this);
        submitBtn.button("loading");

        $.ajax({
            url: "/api/comments/" + commentId,
            type: "Delete",
            success: function () {
                submitBtn.button("reset");

                // change reactingNumber
                var reactingNumber = commentElement.parents(".post").find(".js-comment .reactingNumber"),
                    currentReactingNumber = parseInt(reactingNumber.text());

                reactingNumber.text(currentReactingNumber - 1);
                removeZeroFromReactBox();

                commentElement.fadeOut("slow", function () {
                    $('#removeCommentModal').modal('toggle');
                    commentElement.remove();
                });

                 
            }

        });
    });





                    
                    /// Likes ///
    // add like
    $(".posts").on("click", ".js-like", function () {

        var likeBtn = $(this);
        postId = likeBtn.parents(".post").attr("data-post-id");

        var reactingNumber = $(this).find(".reactingNumber"),
            currentReactingNumber = parseInt(reactingNumber.text());

        $.ajax({
            url: "/api/likes/",
            type: "post",
            dataType: "json",
            data: {
                postId: postId
            },
            success: function (like) {
                likeBtn
                    .attr("data-like-id", like.id)
                    .addClass("activeLike")
                    .addClass("js-unLike")
                    .removeClass("js-like");

                reactingNumber.text(currentReactingNumber + 1);
                removeZeroFromReactBox();
            }
        });

    });

    // remove like
    $(".posts").on("click", ".js-unLike", function () {

        var unLikeBtn = $(this);
        likeId = unLikeBtn.attr("data-like-id");

        var reactingNumber = $(this).find(".reactingNumber"),
            currentReactingNumber = parseInt(reactingNumber.text());

        $.ajax({
            url: "/api/likes/" + likeId,
            type: "delete",
            success: function () {
                unLikeBtn
                    .removeAttr("data-like-id")
                    .removeClass("activeLike")
                    .removeClass("js-unLike")
                    .addClass("js-like");

                reactingNumber.text(currentReactingNumber - 1);
                removeZeroFromReactBox();
            }
        });

    });

                        
                    /// loves ///
    // add love
    $(".posts").on("click", ".js-love", function () {

        var loveBtn = $(this);
        postId = loveBtn.parents(".post").attr("data-post-id");

        var reactingNumber = $(this).find(".reactingNumber"),
            currentReactingNumber = parseInt(reactingNumber.text());

        $.ajax({
            url: "/api/loves/",
            type: "post",
            dataType: "json",
            data: {
                postId: postId
            },
            success: function (love) {
                loveBtn
                    .attr("data-love-id", love.id)
                    .addClass("activeLove")
                    .addClass("js-unLove")
                    .removeClass("js-love");

                reactingNumber.text(currentReactingNumber + 1);
                removeZeroFromReactBox();
            }
        });

    });

    // remove Love
    $(".posts").on("click", ".js-unLove", function () {

        var unLoveBtn = $(this);
        loveId = unLoveBtn.attr("data-love-id");

        var reactingNumber = $(this).find(".reactingNumber"),
            currentReactingNumber = parseInt(reactingNumber.text());

        $.ajax({
            url: "/api/loves/" + loveId,
            type: "delete",
            success: function () {
                unLoveBtn
                    .removeAttr("data-love-id")
                    .removeClass("activeLove")
                    .removeClass("js-unLove")
                    .addClass("js-love");

                reactingNumber.text(currentReactingNumber - 1);
                removeZeroFromReactBox();
            }
        });

    });


});


