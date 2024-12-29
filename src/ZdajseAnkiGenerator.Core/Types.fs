module Types

type Answer = {
    answer: string
    correct: bool
}

type QuizData = {
    question: string
    id: int32
    numberOfComments: int32
    answers: Answer array
}

type Schema = {
    id: string
    title: string
    data: QuizData array
}