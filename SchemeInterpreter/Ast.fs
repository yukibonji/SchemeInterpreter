module Ast

open System

type ThrowsError<'a> = Choice<'a, LispError>

and LispVal =
    | Atom of string
    | List of LispVal list
    | DottedList of LispVal list * LispVal
    | Number of int
    | String of string
    | Bool of bool
    | Float of double
    | PrimitiveFunc of (LispVal list -> ThrowsError<LispVal>)
    | CodedFunc of FuncInfo
    | Port of System.IO.Stream
    | IOFunc of (LispVal list -> ThrowsError<LispVal>)

and Env = Map<string, LispVal>

and MutEnv = { mutable env : Env }

and FuncInfo = { parameters : string list; vararg : string option; body : LispVal list; closure : Env }

and LispError =
    | NumArgs of int * LispVal list
    | TypeMismatch of string * LispVal
    | BadSpecialForm of string * LispVal
    | NotFunction of string * string
    | UnboundVar of string * string
    | ParserError of string
    | PortError of Exception
    
type LispProgram = Prog of LispVal list