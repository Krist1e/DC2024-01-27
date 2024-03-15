package by.bsuir.rv.service.comment;

import by.bsuir.rv.dto.CommentRequestTo;
import by.bsuir.rv.dto.CommentResponseTo;
import by.bsuir.rv.exception.EntititesNotFoundException;
import by.bsuir.rv.exception.EntityNotFoundException;

import java.math.BigInteger;
import java.util.List;

public interface ICommentService {
    List<CommentResponseTo> getComments() throws EntititesNotFoundException;
    CommentResponseTo addComment(CommentRequestTo comment);
    void deleteComment(BigInteger id) throws EntityNotFoundException;
    CommentResponseTo updateComment(CommentRequestTo comment) throws EntityNotFoundException;
    CommentResponseTo getCommentById(BigInteger id) throws EntityNotFoundException;
}
