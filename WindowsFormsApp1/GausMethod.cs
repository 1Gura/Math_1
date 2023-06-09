class GausMethod
{
    public uint RowCount;
    public uint ColumCount;
    public double[][] Matrix { get; set; }
    public double[] RightPart { get; set; }
    public double[] Answer { get; set; }

    public GausMethod(uint Row, uint Colum)
    {
        RightPart = new double[Row];
        Answer = new double[Row];
        Matrix = new double[Row][];
        for (int i = 0; i < Row; i++)
            Matrix[i] = new double[Colum];
        RowCount = Row;
        ColumCount = Colum;

        //������� ������
        for (int i = 0; i < Row; i++)
        {
            Answer[i] = 0;
            RightPart[i] = 0;
            for (int j = 0; j < Colum; j++)
                Matrix[i][j] = 0;
        }
    }

    private void SortRows(int SortIndex)
    {

        double MaxElement = Matrix[SortIndex][SortIndex];
        int MaxElementIndex = SortIndex;
        for (int i = SortIndex + 1; i < RowCount; i++)
        {
            if (Matrix[i][SortIndex] > MaxElement)
            {
                MaxElement = Matrix[i][SortIndex];
                MaxElementIndex = i;
            }
        }

        //������ ������ ������������ ������� ������ ��� �� ������� �����
        if (MaxElementIndex > SortIndex)//���� ��� �� ������ �������
        {
            double Temp;

            Temp = RightPart[MaxElementIndex];
            RightPart[MaxElementIndex] = RightPart[SortIndex];
            RightPart[SortIndex] = Temp;

            for (int i = 0; i < ColumCount; i++)
            {
                Temp = Matrix[MaxElementIndex][i];
                Matrix[MaxElementIndex][i] = Matrix[SortIndex][i];
                Matrix[SortIndex][i] = Temp;
            }
        }
    }

    public int SolveMatrix()
    {
        if (RowCount != ColumCount)
            return 1; //��� �������

        for (int i = 0; i < RowCount - 1; i++)
        {
            SortRows(i);
            for (int j = i + 1; j < RowCount; j++)
            {
                if (Matrix[i][i] != 0) //���� ������� ������� �� 0, �� ���������� ����������
                {
                    double MultElement = Matrix[j][i] / Matrix[i][i];
                    for (int k = i; k < ColumCount; k++)
                        Matrix[j][k] -= Matrix[i][k] * MultElement;
                    RightPart[j] -= RightPart[i] * MultElement;
                }
                //��� �������� �������� �������� ������ ���������� ������ ���
            }
        }

        //���� �������
        for (int i = (int)(RowCount - 1); i >= 0; i--)
        {
            Answer[i] = RightPart[i];

            for (int j = (int)(RowCount - 1); j > i; j--)
                Answer[i] -= Matrix[i][j] * Answer[j];

            if (Matrix[i][i] == 0)
                if (RightPart[i] == 0)
                    return 2; //��������� �������
                else
                    return 1; //��� �������

            Answer[i] /= Matrix[i][i];

        }
        return 0;
    }



    public override String ToString()
    {
        String S = "";
        for (int i = 0; i < RowCount; i++)
        {
            S += "\r\n";
            for (int j = 0; j < ColumCount; j++)
            {
                S += Matrix[i][j].ToString("F04") + "\t";
            }

            S += "\t" + Answer[i].ToString("F08");
            S += "\t" + RightPart[i].ToString("F04");
        }
        return S;
    }
}

